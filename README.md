# TECHIN_sudormrf

Egzamino projektas

## Projekto paleidimas

#### 1. Docker konteinerio sukūrimo komanda

```
docker run --name App -e MYSQL_ROOT_PASSWORD=root -d -p 3306:3306 mysql:lts
```

### DB migracijos komandos

#### Migracijų atnaujinimas rankiniu būdu

```
dotnet ef database update
```

#### Migracijos pridėjimas

*\*leidžiama iš projekto root direktorijos*  
*\*Čia pavyzdys. Kuriant naują migraciją reikia pakeisti pavadinimą*
```
dotnet ef migrations add UpdateUserTable -p ./App.Data/ -s ./App.API/
```

### Swagger nuoroda

http://localhost:5141/swagger/index.html
