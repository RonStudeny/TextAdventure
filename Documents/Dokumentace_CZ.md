# **Dokumentace hry Text Adventure**
### Verze:
1.0
### Obor:
INFOP
### Předmět:
Programování
### Autor:
Ron Studený
### Kontakt:
XSTUR008@studenti.czu.cz

# 1 Základní informace
## 1.1 Cíl a koloběh hry
Jedná se o textovou hru ve které hráč činí rozhodnutí na základě situací ve kterých se vyskytuje. Hráč se vždycky vyskytuje v jakési lokaci, která mu je popsána a má na výběr z několika možností.<br>
Hráč může lokaci několikrát prohledat, výsledkem čehož je buďto nalezení předmětu odpovídajícího dané lokalitě (zbraň, consumable či jiné předměty), nebo může narazit na nepřítele se kterým musí bojovat, nebo může riskovat útěk, který může (ale nemusí vyjít).<br>
Jakmile je lokace zbavena všech míst k prohledání, hráč je vybídnut, aby změnil lokalitu, chce-li hledat něco dalšího. Tímto procesem hráč postupně prozkoumává svět, získává lepší předměty a bojuje o přežití.
## 1.2 Rozsah
Z funkčního hlediska obsahuje hra všechny základní funkcionality do takového rozsahu, aby plnila zadání semestrální práce.<br>
**Neobsahuje** však veškerý obsah který jsem původně zamýšlel, tím je myšleno: **vytváření předmětů (crafting), komplexnější prohledávání lokalit, komplexnější bojový systém, postupně se stupňující průběh hry (obtížnost, vzácnost předmětů a nepřátel...), větší množství možných předmětů a lokalit, atd.**
## 1.3 Ovládání
Průběh většiny interakcí mezi hrou a hráčem probíhají tímto způsobem:<br>
Hra vypíše jakýsi podnět například uvítací zprávu hlavního menu, a vypíše několik očíslovaných možností, ze kterých si hráč může vybrat napsáním korespondujícího čísla.<br><br>

Vítejte ve hře TextAdventure<br>
zvole prosím možnost:<br>
1. Nová hra<br>
2. Načti hru<br>
3. Ukončit hru<br>

# 2 Struktura progamu
## 2.1 Funkční specifikace
### 2.1.1 Třída Game
Třída obsahující entrypoint programu a udržuje data potřebná k běhu hry<br><br>
``public static GameData currentGame`` pole obsahující veškerá data se kterými se pracuje během **načítání**, **ukládání** a **průběhu** hry<br><br>
``Main()`` Entry point programu, pouze spouští další statickou metodu, která zajišťuje spuštění hlavního menu.
### 2.1.2 Třída GameInteractor
*popis*<br><br>
``private static Dictionary<int, Action> _responseDict`` pole typu Dictionary, určeno k časté redefinici, používá se k spuštění korespondující metody na základě celočíselného klíče (z pravidla vstup uživatele)
#### 2.1.2.1 Interakce s menu
Funkce potřebné k implementování základních funkcionalit různých menu<br><br>
``MainMenu()`` Funkce spuštěna z entry poinutu, dá hráči na výběr několik základních možnistí hlavního menu: **Začít novou hru, načíst existující hru, otevřít pomocné menu a odejít ze hry**<br><br>
``NewGame()`` Zařídí vše potřebné ke spuštění nové hry, aniž by vytvořila nebo přepsala jakékoliv existující soubory s uloženými daty<br><br>
``SaveGame()`` Provede hráče procesem vytváření nového .json souboru ve kterém budou uložena veškerá data momentálně rozehrané hry <br><br>
``LoadGame()`` Zobrazí veškeré souobory typu .json uložené ve složce určené pro ukládání her, umožní hráči zvolit soubor, který chce načíst a odchytne veškeré chyby, které by mohly způsobit selhání programu, například v případě neúplných či chybných dat <br><br>
``Help()`` zatím neimplementováno<br><br>
``Exit(bool showWarning = false)`` Funkce sloužící k ukončení programu/hry, nepovinný parametr ``showWarning`` slouží k určení, zda by se po hráči mělo vyžadovat potvrzení (např. pokud by ukončoval hru před uložením) <br>
#### 2.1.2.2 Interakce se hrou
Funkce implementující hlavní chod a funkcionality hry<br><br>
``GameLoop()`` Hlavní "cyklus" hry, dává hráči na výběr z několika možností: **Změnit lokaci, prohledat lokaci, zobrazit inventář a odejít ze hry** <br><br>
``ChangeLocation()`` Změní lokaci hráče tím, že Modifikuje aktivní instanci hry, přesněji informace o aktuální hračově lokalitě a lokalitě, na kterou se může dál přesunout. Informace o aktuální lokalitě přepíše informacemi o té další a vygeneruje data pro další lokaci, kterými přepíše ta původní <br><br>
``SearchLocation(int chances)`` Hráč "prohledá" lokaci, výsledkem čehoč může být nalezení nějakého předmětu (funkce ``GrantItem()``) nebo utkání s nepřítelem (funkce ``Fight()``). Parametr ``chances`` slouží k stanovení, jak velkou šanci má hráč na nalezení předmětu/nepřítele (pokud ``chances`` = 1 - 100% nepřítel, 2 - 50%, 4 - 25%...) <br><br>
``GrantItem()`` Vybere náhodný předmět, ze seznamu předmětů které se v dané lokaci mohou vyskytovat a hráč si vybere zda ho zahodit či přidat do inventáře<br><br>
``Fight()`` Vybere náhodného nepřítele ze seznamu nepřátel, kteří se mohoou v dané lokaci vyksytovat, hráč je dále "zamknut" v módu boje, kde má tři možnosti: **Bojovat, použít předmět nebo utéct**, boj končí v případě že hráč nepřítele porazí, hráč zemře, nebo úspěšně uteče<br><br>
``ShowInventory()`` Vypíše obsah hráčova invenáře, rozdělený na skupiny podle využití předmětů (zbraň, konzumovatelný předmět, suroviny)<br><br>
``GameOver()`` Funkce která se spustí pokud hráč zemře v boji, umožní odejít ze hry či načíst uloženou hru zavoláním ``LoadGame()`` funkce<br><br>
### 2.1.3 Třída FileAccess
Implementace statických metod pro (de)serializaci a čtení/zápis do souboru<br><br>
``SaveGameToFile(string fileName, GameData game, out Exception? e)`` Serializuje herní data z ``game`` parametru, a pokusí se je uložit pod ``fileName`` jménem, pokud ukládání či serializace selže, bude kód podchycen a chyba bude vráce out parametrem ``e``. Při úspěšném průběhu funkce vrací hodnotu ``true`` jinak vrací hodnotu ``false``<br><br>
``LoadFromFIle(string filePath, out GameData game, out Exception e)`` Deserializuje herní data ze souboru nalezeného podle ``filePath`` parametru, data vrátí prostřednictvím out parametru ``game`` a pokud deserializace či nalezení souboru selže, vrátí chybové hlášení prostřednictvím out parametru ``e``. Při úspěšném průběhu funkce vrací hodnotu ``true`` jinak vrací hodnotu ``false``<br><br>
### 2.1.4 Třída Helpers
Třída implementuje statické funkce které se opakovaně využívají na různých místech v programu, či jejichž definice není žádoucí v daném místě v kódu z důvodu přehlednosti<br><br>
``Conversation(string message, List<string> options, out int res, bool backOption, string backText = "Back")`` Tvoří páteř interakce mezi uživatelem a hrou, parametrem ``message`` funkce vypíše podnět, na který dá hráči následně možnost reagovat tím, že vypíše a očísluje seznam možností, který je poskytnut parametrem ``options``, uživatel reaguje napsáním korespondujícího čísla, od tohoto vstupu je odečtena 1, aby korespondovalo s indexy v poskytnutém seznamu a vráceno prostřednictvím out parametru ``res``. parametr ``backOption``určuje, zda-li by mezi možnosti měla být přidána možnost "zpět", pokud ano a tato možnost je zvolena, do ``result`` paramatru je uložena hodnota -1. Nepovinný parametr ``backText`` ovlivňuje pojmenování této zpátečné možností<br><br>
``GetNewLocation()``Vybere náhodnou lokaci ze seznamu implementovaných lokací a inicializuje jeji novou instanci, kterou vrátí<br><br>
``GetItemsOfType<itemType>(List<Item> items)`` Generická funkce která z listu ``items`` vybere a vrátí položky, které jsou daného typu dle dědičnosti, požadovaný typ je určen parametrem  ``<itemType>``<br><br>
``GetNames<itemType>(List<itemType> items)`` Vrací seznam typu ``string``, z listu ``items`` zavolá u každé položky ``.ToString()`` metodu a přidá výsledek do výsledného seznamu<br><br>
``GetFileNames(List<string> paths)``Získá jméma souborů z konce cest poskytnutých ``paths`` parametrem a vrátí je formou seznamu<br>
## 2.3 Datové struktury
popis<br><br>
### 2.3.1 Třída GameData
**popis**<br><br>
### 2.3.2 Třída Player
**popis**<br><br>
### 2.3.3 Třída Item
**popis**<br><br>
#### 2.3.3.1 Třída Weapon : Item
**popis**<br><br>
#### 2.3.3.2 Třída Weapon : Item
**popis**<br><br>
#### 2.3.3.3 Třída Weapon : Item
**popis**<br><br>
### 2.3.4 Třída Enemy
**popis**<br><br>
### 2.3.5 Třída Location
### 2.3.6 Třída Templates
Třída poskytuje instance br><br>
**popis**<br><br>
## 2.3 Popis průběhu programu
## 2.4 Závislosti
## 2.5 Nedostatky a možná zlepšení
# 3 Závěr


