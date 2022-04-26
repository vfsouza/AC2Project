int l0 = 13, l1 = 12, l2 = 11, l3 = 10;
int  x, y, w;
int pos;
String operations[100];
String op;

void setup() {
  pinMode(l0, OUTPUT);
  pinMode(l1, OUTPUT);
  pinMode(l2, OUTPUT);
  pinMode(l3, OUTPUT);

  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0) {
    op = Serial.readString();

    pos = operations[0].toInt();
    if (pos == 0) {
      operations[0] = "4";
      pos = 4;
    }

    if (op.length() >= 3 && op.equals("fim") == 0) {
        operations[pos] = op;
        setOperations();

        serialPrint(operations, pos);
    } else {
      for (int i = 4; i < operations[0].toInt(); i++) {
        x = hexToInt(operations[i].charAt(0));
        y = hexToInt(operations[i].charAt(1));
        w = hexToInt(operations[i].charAt(2));

      	ledsPrint(operation(x, y, w));
        delay(2000);
      }
      exit(0);
    }
  }
}

void setOperations(){
    clearArray();

    x = hexToInt(operations[pos].charAt(0));
    operations[2] += operations[pos].charAt(0);

    y = hexToInt(operations[pos].charAt(1));
    operations[3] += operations[pos].charAt(1);
      
    w = hexToInt(operations[pos].charAt(2));  
    operations[1] += intToHex(operation(x, y, w));
    operations[0] = String(operations[0].toInt() + 1);
}

void clearArray(){
    operations[1] = "";  
    operations[2] = "";
    operations[3] = "";
}

int hexToInt(char h) {
  int retorno = -1;
  if (h >= 48 && h <= 57) {
    retorno = (int)(h - 48);
  } else if (h >= 65 && h <= 70) {
    retorno = (int)(h - 55);
  }

  return retorno;
}

char intToHex(int i) {
  char retorno = '0';
  if (i >= 0 && i <= 9) {
    retorno = (char)(i + 48);
  } else if (i >= 10 && i <= 15) {
    retorno = (char)(i + 55);
  }

  return retorno;
}

int operation(int a, int b, int op) {
  int retorno = 0;
  switch (op) {
  case 0:
    retorno = notNum(a);
    break;
  case 1:
    retorno = notNum(orAB(a, b));
    break;
  case 2:
    retorno = andAB(notNum(a), b);
    break;
  case 3:
    retorno = 0;
    break;
  case 4:
    retorno = notNum(andAB(a, b));
    break;
  case 5:
    retorno = notNum(b);
    break;
  case 6:
    retorno = xorAB(a, b);
    break;
  case 7:
    retorno = andAB(a, notNum(b));
    break;
  case 8:
    retorno = orAB(notNum(a), b);
    break;
  case 9:
    retorno = notNum(xorAB(a, b));
    break;
  case 10:
    retorno = b;
    break;
  case 11:
    retorno = andAB(a, b);
    break;
  case 12:
    retorno = 1;
    break;
  case 13:
    retorno = orAB(a, notNum(b));
    break;
  case 14:
    retorno = orAB(a, b);
    break;
  case 15:
    retorno = a;
    break;
  }

  return retorno;
}

int notNum(int num){
    return 15 - num;
}

int orAB(int a, int b){
    return a | b;
}

int xorAB (int a, int b){
    return a ^ b;
}

int andAB (int a, int b){
    return a & b;
}

void ledsPrint(int res) {
  if (res >= 8) {
    res -= 8;
    digitalWrite(l0, HIGH);
  } else {
  	digitalWrite(l0, LOW);
  }
  
  if (res >= 4) {
    res -= 4;
    digitalWrite(l1, HIGH);
  } else {
  	digitalWrite(l1, LOW);
  }
  
  if (res >= 2) {
    res -= 2;
    digitalWrite(l2, HIGH);
  } else {
  	digitalWrite(l2, LOW);
  }

  digitalWrite(l3, res == 0 ? LOW : HIGH);
}

void serialPrint(String operations[100], int tam) {
  Serial.print("->|");
  for (int i = 0; i <= tam; i++) {
    Serial.print(operations[i]);
    Serial.print("|");
  }
  Serial.print("\n");
}