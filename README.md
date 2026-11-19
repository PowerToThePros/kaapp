# Kunden Artikel APP
## A Simple ASP.NET CORE Web API

## !! TODO
Rechtsklick auf das kaap Projekt in der IDE und Manage User Secrets
die File sollte mindestens so aussehen, PORT, USERNAME, PASSWORD und der DATABASE_NAME müssen natürlich gesetzt werden

{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Port=<PORT>;Username=<USERNAME>;Password=<PASSWORD>;Database=<DATABASE_NAME>"
    }
}

Des weiteren wird eine lokaler PostgreSQL Server benötigt 

Zum Managen der Datenbank empfehle ich pgAdmin 4

## Architektur Schema
Controller -> Manager -> Business -> Data

### Weiteres 
**! Anwendung muss lokal laufen !**

API Dokumentation erreichbar unter https://localhost:7133/openapi/v1.json

Endpoints Testen unter https://localhost:7133/scalar
