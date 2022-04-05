# BattleshipSimulation

1. Założenia projektowe

Ustalenie początkowych założeń projektu zajęło trochę czasu ponieważ ilość wariancji zasad jest spora, więc zdecydowałem się wybrać najbardziej podstawowe:
- 5 statków (Carrier 5 kratek, battleship 4 kratki, cruisier 3 kratki, submarine 3 kratki i destroyer 2 kratki)
- statki mogą do siebie przylegać 
- po trafieniu gracz NIE ma następnego strzału
- gra kończy się po zatopieniu wszystkich statków przez jedną ze stron

2. Ustalenie strategii

Początkowo projekt miał nie traktować statku jako całego obiektu, a miał rozkładać statek na części. Jednak w dalszej fazie projektu zdecydowałem się odejść od takiego
rozwiązania i zacząłem traktować statek jako całość, a planszę "rozebrałem" na poszczególne elementy (komórki), które mają swój status. Dzięki temu łatwiej jest dostosować program
do rozstawiania okrętów, odczytywania miejsca strzału i przypisywania miejsca strzału. 

Kolejnym elementem jest sama symulacja rozgrywki. 2 komputery w losowy sposób rozmieszczają okręty i tura po turze oddają strzały. Przebieg każdej tury jest widoczny w konsoli 
wraz z podglądem na 2 planszach dla każdego PC: jedna z okrętami rozstawionymi przez PC druga z widokiem gdzie padały strzały. Komputer strzela na początku w całkowicie losowe miejsce,
jednak po trafieniu zaczyna szukać okrętu przeciwnika w górę/dół lub prawo/lewo gdyż tak zachowałby się człowiek. Skraca to znacznie rozgrywkę niż w przypadku gdyby każdy strzał 
był całkowicie losowy.

3. Implementacja 

W trakcie implementacji natknąłem się na problem z czytelnością w trakcie wyświetlania plansz. Nie mogłem rozróżnić jaki typ okrętu znajduje się na planszy, a w dalszym etapie 
rozgrywki plansza z widokiem oddanych strzałów stawała się coraz bardziej nieczytelna. Dlatego też każdy ze statków nadaje inny status komórce (np. jeżeli jest to destroyer to komórka 
wyświetli D). Ponadto na planszy ze strzałami znaczek trafienia czyli X jest czerwony, a znaczek pudła czyli O jest niebieski.

3.1 Klasy pomocnicze

Znak wyświetlany na tablicach gry jest przypisywana za pomocą atrybutu do stanu danego pola. Aby wyłuskać konkretny znak potrzebny do wyświetlenia dla instancji obiektu klasy cell
została stworzona metoda rozszerzająca odczytująca wartość atrybutu Description.

Inne extention methods służą do przeszukiwania w efektywny sposób plansz.
