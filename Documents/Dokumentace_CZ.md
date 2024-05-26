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
``private static Dictionary<int, Action> _responseDict`` pole určeno k časté redefinici, používá se k spuštění korespondující metody na základě celočíselného klíče (z pravidla vstup uživatele)
#### 2.1.2.1 Interakce s menu
``MainMenu()``<br>
``NewGame()``<br>
``SaveGame()``<br>
``LoadGame()``<br>
``Help()``<br>
``Exit()``<br>
#### 2.1.2.2 Interakce se hrou
``GameLoop()``<br>
``ChangeLocation()``<br>
``SearchLocation()``<br>
``GrantItem()``<br>
``Fight()``<br>
``ShowInventory()``<br>
``GameOver()``<br>
### 2.1.3 Třída FIleAccess
*popis*<br>
``SaveGameToFile()``
``LoadFromFIle()``
### 2.1.4 Třída Helpers
``Conversation()``
``GetNewLocation()``
``GetItemsOfType()``
``GetNames()``
``GetFileNames()``
## 2.3 Datové struktury
## 2.2 Průběh
## 2.4 Závislosti


