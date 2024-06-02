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

# Obsah
* 1 [Základní informace](#1-základní-informace)
  * 1.1 [Cíl a koloběh hry](#11-cíl-a-koloběh-hry)
  * 1.2 [Rozsah](#12-rozsah)
  * 1.3 [Ovládání](#13-ovládání)
* 2 [Struktura progamu](#2-struktura-progamu)
  * 2.1 [Funkční specifikace](#21-funkční-specifikace)
    * 2.1.1 [Třída Game](#211-třída-game)
    * 2.1.2 [Třída GameInteractor](#212-třída-gameinteractor)
      * 2.1.2.1 [Interakce s menu](#2121-interakce-s-menu)
      * 2.1.2.2 [Interakce se hrou](#2122-interakce-se-hrou)
    * 2.1.3 [Třída FileAccess](#213-třída-fileaccess)
    * 2.1.4 [Třída Helpers](#214-třída-helpers)
  * 2.3 [Datové struktury](#23-datové-struktury)
    * 2.3.1 [Třída GameData](#231-třída-gamedata)
    * 2.3.2 [Třída Player](#232-třída-player)
    * 2.3.3 [Třída Item](#233-třída-item)
      * 2.3.3.1 [Třída Weapon : Item](#2331-třída-weapon--item)
      * 2.3.3.2 [Třída Consumable : Item](#2332-třída-consumable--item)
      * 2.3.3.3 [Třída CraftItem : Item](#2333-třída-craftitem--item)
    * 2.3.4 [Třída Enemy](#234-třída-enemy)
    * 2.3.5 [Třída Location](#235-třída-location)
    * 2.3.6 [Třída Templates](#236-třída-templates)
    * 2.3.7 [Třída TextSource](#237-třída-textsource)
  * 2.4 [Koncepční průběh programu](#24-koncepční-průběh-programu)
    * 2.4.1 [Hlavní menu](#241-hlavní-menu)
    * 2.4.2 [Hra](#242-hra)
    * 2.4.3 [Boj](#243-boj)
  * 2.5 [Závislosti](#25-závislosti)
    * 2.5.1 [Newtonsoft.Json](#251-newtonsoftjson)
  * 2.6 [Nedostatky a možná zlepšení](#26-nedostatky-a-mozná-zlepšení)
    * 2.6.1 [Práce s herními daty a textem](#261-práce-s-herními-daty-a-textem)
    * 2.6.2 [Obsah hry](#262-obsah-hry)
    * 2.6.3 [Vstupní omezení](#263-vstupní-omezení)

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
``IsFileNameValid(string fileName)`` Ověří správnost formátu jména souboru přijatého argumentem ``fileName``
## 2.3 Datové struktury
Třídy a objekty, které reprezentují jednotlivé součásti hry, jejich rozdělení, dědičnost a význam...
### 2.3.1 Třída GameData
Obsahuje veškeré informace potřebné k chodu hry, instance této třídy se používá jakožto aktivní zdroj dat, které se za chodu hry modifikují a dále používají. Zároveň je to instance této třídy která se serializuje a deserializuje při ukládání/načítání hry<br><br>
``public Player player`` Souhrn aktuálních dat o hráči<br><br>
``public Location location`` Souhrn dat o aktuální lokaci hráče<br><br>
``public Location nextLocation`` Souhrn dat o další přístupné lokaci<br><br>
``public GameData game`` 
### 2.3.2 Třída Player
Třída reprezentující hráče a jeho vlastnostibr><br>
``public int Health`` Celé číslo reprezentující hráčovo zdraví<br><br>
``public List<Item> Items`` Seznam instnací typu ``Item`` reprezentující hráčův inventář <br><br>
### 2.3.3 Třída Item
Základní třída ze které dědí následující třídy, obsahuje vlastnoti společné pro všehcny objektu typu ``Item``<br>
Všechny třídy dědící z této třídy implementují přetížený konstruktor určený k vytvoření identické instacne poskytnuté třídy, což slouží k ukládání více předmětů stejného typu.
``override string ToString()`` přetížení ``.ToString()`` metody za účelem jednoduchého vypsání jednotlivých položek, například v případě inventáře, je taktéž implementována u všech tříd dědících z této třídy.<br><br>
``public string Name`` Jméno předmětu<br><br>
``public string Description`` Popis předmětu zobrazen při jeho nalezení<br><br>
#### 2.3.3.1 Třída Weapon : Item
Druh předmětu používaný jako zbraň, má vymezený počet použití, a určité poškození, které může udělit nepříteli<br><br>
``public int Damage`` Celočíselná hodonota reprezentující poškození udělené nepříteli při použití tohoto předmětu<br><br>
``public int Uses`` Hodnota reprezentující počet, kolikrát se předmět dá použít než se "rozbije" <br><br>
#### 2.3.3.2 Třída Consumable : Item
Předmět sloužící k doplnění hráčova zdraví<br><br>
``public int HealthRestore`` Množství zdraví, které bude hráči doplněno po použítí předěmetu, není možné přesáhnout hodnotu zdraví 100<br><br>
``public int Uses`` Hodnota reprezentující počet, kolikrát se předmět dá použít než se "vypotřebuje" <br><br>
#### 2.3.3.3 Třída CraftItem : Item
Předměty tohoto typu lze ve hře nalézt ale aktuálně nemají žádný účel, jelikož vyrábění **(crafting)** nebylo implementováno<br><br>
### 2.3.4 Třída Enemy
Všechna data relevantní k nepříteli, se kterým se hráč uktá<br>
Obsahuje přetížený konstruktor určený k vytvoření nové instance podle "šablony"<br><br>
``public string Name`` Jméno nepřítele, opakovaně se zobrazuje během boje<br><br>
``public int Health`` Zdraví nepřítele, může být i hodnota větší než 100<br><br>
``public int Damage`` Poškození, které nepřítel udělí hráči na konci jeho tahu v utkání<br><br>
### 2.3.5 Třída Location
Definuje vlastnosti lokace, ve které se hráč může či aktuálně nachází.<br>
Obsahuje přetížený konstruktor určený k vytvoření nové instance podle "šablony"<br><br>
``public string Name`` Jméno lokace
``public string Narrative`` Popis zobrazen při změně lokace, slouží k nastínění a popisu lokace
``public int Searches`` Kolikrát může hráč ještě prohledat lokalitu.
``public int SearchChances`` Jakou šanci má hráč najít nepřítele při prohledávání lokality (1 = 100% nepřítel, 2 = 50%, 4 = 25% ...)
``public Item[] ItemPool`` Pole obsahující odkazy na instance předmětů, které je možné nalézt v dané lokaci
``public Enemy[] EnemyPool`` Pole obsahující odkazy na instance nepřátel, kteří se mohou vyskytovat v dané lokaci
### 2.3.6 Třída Templates
Třída poskytuje specifické instance výše definovaných tříd, které tvoří celkový obsah hry, je definováno několik předmětů, nepřátel a lokalit, avšak jen v takovém množštví, aby hra byla testovatelná <br><br>
``public Location[] locations`` Pole obsahující všechny momentálně implementované lokace
### 2.3.7 Třída TextSource
Třída obsahuje veškerý text, který je ve hře zobrazován. Je uložen prostřednictvím statických ``string`` proměnných a statických ``string`` polí, která zpravidla reprezentují hráčovi možnosti odpovědi na podnět
## 2.4 Koncepční průběh programu
### 2.4.1 Hlavní menu
Po způštění je hráč uvítán hlavním menu, ve kterém si může vybrat z několika možností: **začít novou hru, načíst hru, menu s pomocným popisem, odejít ze hry**
### 2.4.2 Hra
Po vytvoření nové hry či načtení hry ze souboru je hráč přesunut do hlavního koloběhu hry, hráč má několik možností: **změna lokace, prohledání lokace, zobrazení inventáře, uložení hry a ukončení hry**
### 2.4.3 Boj
Pokud hráč během prohledání objeví nepřítele, je přesunut do módu "boje", který je založený na tazích, hráč si opět může vybírat z několika možností: **zaútočit, použít předmět, utéct**
1. při **útoku** si hráč vybere jednu ze zbraní, kterou doposud našel či může své rozhodnutí zrušit, pokud se rozhodne zaútočit, hráčův tah končí a nepřítel zaútočí také, pokud své rozhodnutí zruší, tah hráči zůstává
2. **použítí předmětu** hráč si může vybrat z doposud nalezených konzumovatelných předmětů, který mu doplní určité množství zdraví (ne víc, než maximum, tedy 100) tato akce neukončí hráčův tah
3. **útěk** Je možné že hráč nemá žádnou či dostatečnou zbraň na obranu, tím pádem je jediná možnost útěk, který může a nemusí vyjít, pokud vyjde, hráč se vrátí zpět do hlavního herního cyklu, pokud ne, skončí hráčův tah a nepřítel zaútočí
## 2.5 Závislosti
### 2.5.1 Newtonsoft.Json
Hra využívá tuto knihovnu k serializaci a deserializaci herních dat do .json formátu
## 2.6 Nedostatky a možná zlepšení
### 2.6.1 Práce s herními daty a textem
První věcí co bych chtěl zlepšit je ten fakt že aktuálně je všechen text využíván ve hře uložen prostřednictvím statických ``string`` proměnných, což způsobuje poměřně velké využití operační paměti na poměrně malou aplikaci. To samé se vztahuje na obsah hry ve formě třídy ``Templates`` který je realizován statickými instancemi různých tříd. Řešení obou těchto případů by bylo uložení veškerých potřebných dat externě a načítat pouze potřebné části.
### 2.6.2 Obsah hry
Obsah hry je aktuálně velmi omezený:
1. z pohledu lokalit, nepřátel, a předmětů které hráč může najít a prozkoumat
2. z pohledu akcí, které hráč může provádět, hra měla původně být rozšířena ještě o postunpné prozkoumávání každé lokality (pod-lokality), dále mělo být soušástí hry vyrábění (crafting) a hlubší bojový systém (brnění, různé efekty od konzumovatelných předmětů i nepřátel apod.)
3. Nekonečný mód. Původní smysl hry měl být v tom, že by byla neustále generována jakýmsi modelem generativní umělé inteligence, která by zastávala roli "vypravěče", tj. generovala by instance všech datových tříd (lokace, předměty, nepřátele...) na základě nějakého počátečního vstupu.
### 2.6.3 Vstupní omezení
Vstupy jsou z většiny ošetřeny a aktuálně mi není známý žádný vstup který by způsobil selhání programu, avšak při zadávání jména souboru pro uložení hry je možné psát různé koncovky, které by potenciálně mohly ovlivnit funkčnost tohoto souboru
