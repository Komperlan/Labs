package Controller.Scenarios;

import Application.Contracts.ICatService;
import Application.Contracts.IOwnerService;
import Application.Objects.Cat;
import Application.Objects.Owner;
import Controller.IScenario;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class ShowOwnerScenario implements IScenario {
    private IOwnerService _ownerService;

    public ShowOwnerScenario(IOwnerService ownerService) {
        _ownerService = ownerService;
    }

    @Override
    public void run() {
        System.out.println("input Owner id: ");
        Scanner in = new Scanner(System.in);

        Owner owner = _ownerService.GetOwner(in.nextInt());
        System.out.println("Name: " + owner.getName());
        System.out.println("Cats: ");
        List<Cat> pets = owner.getCats();

        for(Cat cat : pets) {
            System.out.println(cat.getName());
        }
    }
}
