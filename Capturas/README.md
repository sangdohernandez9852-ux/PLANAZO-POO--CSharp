# Guía para tomar las capturas

Las capturas deben guardarse en esta carpeta con los nombres indicados. El README principal ya tiene los enlaces preparados.

## Recomendaciones

- Ejecuta la aplicación desde la carpeta `PlanazoApp` con `dotnet run`.
- Después de cada acción, la aplicación pide presionar ENTER. Aprovecha ese momento para tomar la captura.
- La aplicación no borra la pantalla, así que puedes desplazarte para mostrar el resultado completo.
- Usa la herramienta de capturas de Windows (`Win + Shift + S`) y guarda las imágenes en formato PNG.
- Si el terminal es muy angosto, las tablas pueden verse desordenadas. Maximiza la ventana antes de capturar.
- Cada vez que cierres y vuelvas a abrir la aplicación, los datos vuelven al estado inicial.

## Caso 1: reserva válida

1. Selecciona la opción `3`.
2. Usuario: `1` (Ana Gómez).
3. Evento: `5` (Taller Cerámica).
4. Cantidad: `2`.

Captura el mensaje de confirmación donde aparece la reserva #1, el valor total de $54.000 y los 13 cupos restantes.

Archivo: `caso-01-reserva-valida.png`

## Caso 2: cupos insuficientes

1. Selecciona la opción `3`.
2. Usuario: `1`.
3. Evento: `3` (Concierto Rock al Parque, 100 cupos).
4. Cantidad: `999`.

Captura el mensaje de error que indica que no hay cupos suficientes.

Archivo: `caso-02-cupos-insuficientes.png`

## Caso 3: cantidad inválida

1. Selecciona la opción `3`.
2. Usuario: `1`.
3. Evento: `1` (Cine Interestelar).
4. Cantidad: `-2`.

Captura el mensaje de error que indica que la cantidad debe ser mayor que cero.

Archivo: `caso-03-cantidad-invalida.png`

## Caso 4: cancelación y devolución de cupos

1. Selecciona la opción `3`.
2. Usuario: `1`.
3. Evento: `5`.
4. Cantidad: `2`.
5. Selecciona la opción `2` y consulta el evento `5`; deben aparecer 13 cupos disponibles.
6. Selecciona la opción `5` y cancela la reserva `1`.
7. Selecciona la opción `2` y consulta nuevamente el evento `5`; deben aparecer 15 cupos disponibles.

Captura el mensaje de cancelación y la consulta final con los 15 cupos.

Archivo: `caso-04-cancelacion-devuelve-cupos.png`

## Caso 5: doble cancelación

Realiza este caso en la misma ejecución del caso 4, después de cancelar la reserva.

1. Selecciona la opción `5`.
2. Reserva: `1`.

Captura el mensaje de error que indica que la reserva ya fue cancelada anteriormente.

Archivo: `caso-05-doble-cancelacion.png`

## Caso 6: evento inexistente

1. Selecciona la opción `2`.
2. Evento: `999`.

Captura el mensaje de error que indica que el evento no existe.

Archivo: `caso-06-evento-inexistente.png`

## Caso 7: precios por tipo de evento

1. Selecciona la opción `1`.

Captura la tabla completa. Deben verse los precios finales:

- Cine Interestelar: $18.000.
- Cine El Padrino: $20.000.
- Concierto Rock al Parque: $57.500.
- Concierto Noche de Jazz: $86.250.
- Taller Cerámica: $27.000.
- Taller Fotografía: $40.500.
- Evento Deportivo Clásico de Fútbol: $44.000.

Archivo: `caso-07-precios-por-tipo.png`

## Captura opcional: resumen

Si quieres evidenciar el resumen, realiza una reserva válida y luego selecciona la opción `6`. Deben aparecer una reserva activa, dos entradas reservadas y un valor total de $54.000.

Archivo sugerido: `caso-08-resumen.png`
