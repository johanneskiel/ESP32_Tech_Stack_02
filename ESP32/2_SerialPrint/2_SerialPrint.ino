void setup() {
  Serial.begin(115200);
}

void loop() {
  Serial.println("hello, world");
  delay(1000);  // 10 Updates pro Sekunde
}