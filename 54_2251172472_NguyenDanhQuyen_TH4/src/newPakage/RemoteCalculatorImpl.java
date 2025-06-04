package newPakage;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;


public class RemoteCalculatorImpl extends UnicastRemoteObject implements RemoteCalculator {
    public RemoteCalculatorImpl() throws RemoteException {
        super();
    }

    @Override
    public int findMax(int[] arr) throws RemoteException {
        if (arr == null || arr.length == 0) {
            throw new RemoteException("Mang rong!");
        }

        int max = arr[0];
        for (int i : arr) {
            if (i > max) max = i;
        }

        System.out.println("Server nhan mang, so lon nhat la: " + max);
        return max;
    }

    @Override
    public int binhphuong(int a) throws RemoteException {
        int result = a*a;
        System.out.println("Client da su dung phuong thuc nay, ket qua: " + result);
        return result;
    }
}
