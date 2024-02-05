import java.util.Scanner;
public class MyClass {
    public static void main(String args[]) {
        Scanner sc = new Scanner(System.in);
        int n = Integer.parseInt(sc.nextLine());
        String[] in = new String[n*2];
        Stack stack1 = new Stack(n);
        String push = ("push");
        String pop = ("pop");
        for(int i = 0; i<n; i++){
            in[i] = sc.nextLine();
        }
        for(int j =0; j<n; j++){
            String pushcheck = " ";
            if(in[j].length() >3){
            pushcheck = in[j].substring(0 ,4);
            }
            if(pushcheck.equalsIgnoreCase(push)){
                stack1.push(Integer.parseInt(in[j].substring(5)));
            }
            else if(in[j].equalsIgnoreCase(pop)&& !stack1.isEmpty()){
                stack1.pop();
            }
        }
        stack1.peek();
        
    }
}
class Stack{
    private final int maxSize;
    private final int[] stackArray;
    private int top;
    public Stack(int maxSize){
        this.maxSize = maxSize;
        stackArray = new int[maxSize];
        top = -1;
    }
    public void push(int i){
        top++;
        stackArray[top] = i;
    }
    public int pop(){
        return stackArray[top--];
    }
    public void peek(){
        System.out.println (stackArray[top]);
    }
    public boolean isEmpty(){
        return (top == -1);
    }
    public boolean isFull(){
        return (top == maxSize-1);
    }
    public void makeEmpty(){
        top = -1;
    }
}
