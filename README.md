# LokalReporterApp
Cross Platform App für Lokalreporter (im moment erstmal android mit dummy daten)

## Projektstruktur

###LokalReporter 
Enthält alle Model-Daten, die alle anderen Projekte kennen müssen (Model-Daten Struktueren / Interfaces für Dienste)

###LokalReporter.Client.Dummy
Dummy Implementierung der Services

###LokalReporter.App.Forms
Xamarin.Forms Anwendung
Enthält hauptsächlich ViewModels, Pages und andere Forms-Controls wenn nötig

Viewmodels verbinden die Logik mit der Anzeige (Pages) über DATENBINDUNG (An Properties lässt sich binden - siehe Label-Text Binding auf der FirstPage.
An Listen lässt sich auch einfach binden (SecondPage)

Viewmodels übernehmen auch die Navigation - ein Viewmodel sagt bescheid, wenn zu einem anderen Viewmodel navigiert werden soll. Die passende Page wird anhand des Namens gewählt (FirstViewModel <> FirstPage, SecondViewModel <> SecondPage etc.)

Wird zu einem Viewmodel navigiert, wird die passende Page geladen, ein Viewmodel erzeugt (Abhängigkeiten werden automatisch in den Konstruktur "injected") und die Methoden Init, sowie Start aufgerufen.
Der BindingContext der neuen Page wird auf die neue Instanz des Viewmodels gesetzt, damit sich die Datenbindungen auflösen können.

Viewmodels sollten von dem BaseViewModel erben. Dann geben Sie der Page automatisch über änderungen von Properties bescheid und die Page updatet sich.

###LokalReporter.App.Droid
Dieses Projekt ist das Startprojekt und wird ausgeführt. 
Hier sollte nichts unnötiges reinkommen, da dies nicht portabel ist. Wenn möglich alles in den anderen Projekten erledigen.

