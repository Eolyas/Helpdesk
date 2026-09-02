# Useful commands

## Go into SQL database
```bash
docker compose exec database psql -U helpdesk_app -d helpdesk
```

## Start docker
```bash
docker compose up -d --build
```

## Show logs
```bash
docker compose logs -f website
```

## Show docker status
```bash
docker compose ps
```

## Update database
```bash
dotnet ef database update
```

```bash
```

```bash
```