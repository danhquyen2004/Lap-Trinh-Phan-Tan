/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Interface.java to edit this template
 */
package newPakage;
import java.rmi.Remote;
import java.rmi.RemoteException;

public interface RemoteCalculator extends Remote{
    int findMax(int[] arr) throws RemoteException;
    int binhphuong(int a) throws RemoteException;
}
