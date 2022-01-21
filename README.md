# MSE3 Microservice Architecture exercise 2

In the context of UAS Technikum Wien Course MSE3 Microservice Architecture.

Exercise description in [documentation](documentation/20220108_mse3_msa_Projekt_2_Aufgabenstellung_2021.pdf).

## Team members
- [@s1gr1d](https://github.com/s1gr1d)
- [@Pipal2k](https://github.com/Pipal2k)
- [@fxal](https://github.com/fxal)

## Description
Goal is to create a hexagonal architecture ticket service meeting the functional requirements stated in the [documentation](documentation/20220108_mse3_msa_Projekt_2_Aufgabenstellung_2021.pdf).

Peripheral components (such as database access, or metrics collection) is abstracted with interfaces in the [Interfaces](Interfaces/) folder. This folder contains the required interfaces.

The domain is defined as set of objects located in the [Domain](Domain/) folder holding entities such as the Ticket itself.

The implementation of the [TicketSellService](TicketSellService.cs) holds the business logic that shall be built in this exercise. It is the entrypoint for an external Adapter, that relays service calls from the user. Three methods are callable:
- ReserveTicket
- BuyTicket
- CancelTicket