# Distance calculator
Calculates distance between 2 points

## Project structure
```
|-- distance-app - the Angular web app
|-- distance-api - dotnet core web api
|-- distance-business - business related logic
    |-- abstractions - some declared and use interfaces
    |-- implementations - some implementations used by web app
    |-- ioc - business related IoC registrations
    |-- implementations-test - some tests of the implementations
|-- distance-contracts - some contract classes used in throughout the app
```

## Prerequisites
* `.net core 2.2 sdk` must be installed on the machine
* `nodeJs` and `npm` has to be installed on the machine

## Usage
* navigate to **distance-app**  in any terminal and type `npm install`. Wait while `npm` download all required packages. In fact this step can be skipped and you can simply go to the next one, but I recommend complete this first step.

* navigate to **distance-api** in any terminal and type `dotnet run`. This will run the whole `app` with the `api` in the backend. Observe the terminal to see what port has been used to launch the app. If you missed that try https://localhost:5001. Now you can use the app in the browser.

* navigate to **implementations-tests** if you want to run some backend tests and type `dotnet test`. This will run several tests. I must admit I was too lazy to write more.

* navigate to **distance-app** if you want to run some frontend tests. Type `npm test`. This will run several frontend tests.
