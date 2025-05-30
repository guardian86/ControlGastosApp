# ControlGastos.Test

Este proyecto contiene pruebas unitarias y de integración para la solución ControlGastos usando MSTest y Moq.

## Estructura sugerida
- Pruebas de servicios (mocks de repositorios)
- Pruebas de controladores (mocks de servicios)
- Pruebas de lógica de negocio

## Ejecutar pruebas

Puedes ejecutar todas las pruebas con:

```
dotnet test
```

## Dependencias
- MSTest
- Moq

## Buenas prácticas
- Usa Moq para simular dependencias.
- Cubre casos de éxito y error.
- Usa nombres descriptivos para los métodos de prueba.
