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
``public static GameData currentGame`` pole obsahující veškerá data se kterými se pracuje během **načítání**, **ukládání** a **průběhu** hry<br>
``Main()`` Entry point programu, pouze spouští další statickou metodu, která zajišťuje spuštění hlavního menu.
### 2.1.2 Třída GameInteractor
*popis*<br><br>
``private static Dictionary<int, Action> _responseDict`` pole typu Dictionary, určeno k časté redefinici, používá se k spuštění korespondující metody na základě celočíselného klíče (z pravidla vstup uživatele)
#### 2.1.2.1 Interakce s menu
Funkce potřebné k implementování základních funkcionalit různých menu<br><br>
``MainMenu()`` Funkce spuštěna z entry poinutu, dá hráči na výběr několik základních možnistí hlavního menu: **Začít novou hru, načíst existující hru, otevřít pomocné menu a odejít ze hry**<br>
``NewGame()`` Zařídí vše potřebné ke spuštění nové hry, aniž by vytvořila nebo přepsala jakékoliv existující soubory s uloženými daty<br>
``SaveGame()`` Provede hráče procesem vytváření nového .json souboru ve kterém budou uložena veškerá data momentálně rozehrané hry <br>
``LoadGame()`` Zobrazí veškeré souobory typu .json uložené ve složce určené pro ukládání her, umožní hráči zvolit soubor, který chce načíst a odchytne veškeré chyby, které by mohly způsobit selhání programu, například v případě neúplných či chybných dat <br>
``Help()`` zatím neimplementováno<br>
``Exit(bool showWarning = false)`` Funkce sloužící k ukončení programu/hry, nepovinný parametr ``showWarning`` slouží k určení, zda by se po hráči mělo vyžadovat potvrzení (např. pokud by ukončoval hru před uložením) <br>
#### 2.1.2.2 Interakce se hrou
Funkce implementující hlavní chod a funkcionality hry<br><br>
``GameLoop()`` Hlavní "cyklus" hry, dává hráči na výběr z několika možností: **Změnit lokaci, prohledat lokaci, zobrazit inventář a odejít ze hry** <br>
``ChangeLocation()`` Změní lokaci hráče tím, že Modifikuje aktivní instanci hry, přesněji informace o aktuální hračově lokalitě a lokalitě, na kterou se může dál přesunout. Informace o aktuální lokalitě přepíše informacemi o té další a vygeneruje data pro další lokaci, kterými přepíše ta původní <br>
``SearchLocation()``<br>
``GrantItem()``<br>
``Fight()``<br>
``ShowInventory()``<br>
``GameOver()``<br>
### 2.1.3 Třída FileAccess
*popis*<br><br>
``SaveGameToFile()``<br>
``LoadFromFIle()``<br>
### 2.1.4 Třída Helpers
*popis*<br><br>
``Conversation()``<br>
``GetNewLocation()``<br>
``GetItemsOfType()``<br>
``GetNames()``<br>
``GetFileNames()``<br>
### 2.1.5 Třída Templates
*popis*<br><br>
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
**popis**<br><br>
## 2.3 Popis průběhu programu
## 2.4 Závislosti
# 3 Závěr


