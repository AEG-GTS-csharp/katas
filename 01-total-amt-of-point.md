# Gesamtanzahl an Punkten

Unsere Fußballmannschaft hat die Meisterschaft beendet.

Die Spielergebnisse unserer Mannschaft sind in einem Array von Zeichenfolgen gespeichert. Jedes Spiel wird durch eine Zeichenfolge im Format „x:y“ dargestellt, wobei x die Punktzahl unserer Mannschaft und y die Punktzahl unserer Gegner ist.

Beispiel: [„3:1“, „2:2“, „0:1“, ...]

Für jedes Spiel werden Punkte wie folgt vergeben:

wenn x > y: 3 Punkte (Sieg)
wenn x < y: 0 Punkte (Niederlage)
wenn x = y: 1 Punkt (Unentschieden)

Wir müssen eine Methode schreiben mit einem String-Array als Parameter,die die  die Anzahl der Punkte zurückgibt, die unsere Mannschaft (x) in der Meisterschaft nach den oben genannten Regeln erzielt hat.

Hinweise:

Unsere Mannschaft bestreitet in der Meisterschaft immer 10 Spiele.
0 <= x <= 4
0 <= y <= 4
