package org.example;

import Application.Objects.Cat;
import Application.Objects.Color;
import Application.Objects.Owner;
import Application.Service.CatService;
import Application.Service.OwnerService;
import Controller.Scenarios.ShowCatScenario;

import java.util.Date;

public class Main {
    public static void main(String[] args) {
        var catService = new CatService();
        var ownerService = new OwnerService();
        var kitty = new Cat();

        Owner owner = new Owner();
        owner.setName("Andrew");
        owner.setId(2L);
        owner.setBirthDate(new Date(2005, 9, 8));
        ownerService.AddOwner(owner);

        kitty.setName("Mers");
        kitty.setOwner(owner);
        kitty.setColor(Color.GRAY);
        kitty.setID(1L);
        kitty.setBreed("Scottish fold");
        kitty.setBirthdate(new Date(2015, 10, 5));

        catService.AddCat(kitty);
        var scenario = new ShowCatScenario(catService);

        scenario.run();
    }
}