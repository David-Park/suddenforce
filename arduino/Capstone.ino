#include <ArduinoJson.h>
#include <SoftwareSerial.h>
#include <IRremote.h>

#define BUTT_PIN 13
#define IR_SEND 3
#define IR_RECEIV 11
#define TXD 1
#define RXD 0

#define IR_VALUE 0x00000008

SoftwareSerial BTSerial(TXD, RXD);
IRsend irsend;
IRrecv irrecv(IR_RECEIV);
decode_results results;

char irvalue[9];
int flag = 0;

StaticJsonBuffer<200> jsonBuffer;

JsonObject& root = jsonBuffer.createObject();

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  BTSerial.begin(9600);

  irrecv.enableIRIn();
  pinMode(BUTT_PIN, INPUT);
  pinMode(IR_SEND, OUTPUT);
  pinMode(IR_RECEIV, INPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(irrecv.decode(&results))//ir receiver
  {
    //Serial.print(results.value, HEX);
    String convert = String(results.value);
    convert.toCharArray(irvalue, sizeof(irvalue));
    root["hit"] = irvalue;
    root.printTo(BTSerial);
    root.printTo(Serial);
    irrecv.resume();
  }
  else
  {
    root["hit"] = "0";
    root.printTo(BTSerial);
    root.printTo(Serial);
  }
  
  if(digitalRead(BUTT_PIN) == LOW) //button & ir_send
  {
    //analogWrite(IR_SEND, 255);
    //digitalWrite(IR_SEND,HIGH);
    irsend.sendSony(IR_VALUE, 4);
    root["shot"] = "true";
    irrecv.enableIRIn();
  }
  else
  {
    root["shot"] = "false";
  }
  
  Serial.println();
}
