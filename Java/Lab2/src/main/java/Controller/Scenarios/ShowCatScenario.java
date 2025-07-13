package Controller.Scenarios;

import Application.Contracts.ICatService;
import Application.Objects.Cat;
import Controller.IScenario;

import java.util.Scanner;

public class ShowCatScenario implements IScenario {
    private ICatService _catService;

    public ShowCatScenario(ICatService catService) {
        _catService = catService;
    }

    @Override
    public void run() {
        System.out.println("input cat id: ");
        Scanner in = new Scanner(System.in);

        Cat kitty = _catService.GetCat(in.nextInt());
        System.out.println("Name: " + kitty.getName());
        System.out.println("Breed: " + kitty.getBreed());
        System.out.println("OwnerName: " + kitty.getOwner().getName());
        System.out.println("BirthDate: " + kitty.getBirthdate().toString());
    }
}
