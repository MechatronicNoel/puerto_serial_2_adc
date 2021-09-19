int ejex, ejey, push;
void setup() {
pinMode(2,INPUT_PULLUP);
Serial.begin(9600);

}

void loop() {

ejex=analogRead(0);
delay(50);

ejey=analogRead(1);
delay(50);

push=digitalRead(2);
delay(50);

Serial.print(ejex);
Serial.print(";");
Serial.print(ejey);
Serial.print(";");
Serial.println(push);

}
