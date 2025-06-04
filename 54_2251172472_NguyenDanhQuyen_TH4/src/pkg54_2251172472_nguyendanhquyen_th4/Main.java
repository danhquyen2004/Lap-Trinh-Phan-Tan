package pkg54_2251172472_nguyendanhquyen_th4;

import newPakage.RemoteCalculator;
import newPakage.RemoteCalculatorImpl;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.Scanner;
public class Main {

    public static void main(String[] args) {
       Server();
    }
    
    public static void Server() 
    {
        try {
            RemoteCalculator calculator = new RemoteCalculatorImpl();

            Registry registry = LocateRegistry.createRegistry(8888);
            registry.rebind("CalculatorService", calculator);

            System.out.println("Server da san sang va cho client...");
        } catch (RemoteException e) {
            e.printStackTrace();
        }
    }
    
    public static void Client() {
        try {
            
            
//            Scanner scanner = new Scanner(System.in);
//            System.out.print("Nhap so phan tu cua mang: ");
//            int n = scanner.nextInt();
//
//            int[] numbers = new int[n];
//            Random rand = new Random();
//            for (int i = 0; i < n; i++) {
//                numbers[i] = rand.nextInt(100) + 1;
//            }
//
//            System.out.print("Mang da tao: ");
//            for (int x : numbers) {
//                System.out.print(x + " ");
//            }
//            System.out.println();
            
              Scanner scanner = new Scanner(System.in);
              System.out.print("Nhap N: ");
              int n = scanner.nextInt();
            

            Registry registry = LocateRegistry.getRegistry("172.20.10.4", 8888);
            RemoteCalculator calculator = (RemoteCalculator) registry.lookup("CalculatorService");

            int result = calculator.binhphuong(n);
            System.out.println("So lon nhat trong mang la: " + result);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
