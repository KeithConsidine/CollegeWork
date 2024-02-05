import java.util.Scanner;
public class MyClass {
    public static void main(String args[]) {
        Scanner sc = new Scanner(System.in);
        Queue theQueue = new Queue(10);
        while(sc.hasNextLine()){
            String[] input = new String[2];
            input[0] = sc.nextLine();
            if(input[0].equals("REMOVE")){
                theQueue.remove();
            }
            else{
                theQueue.insert(input[0].split(" ")[1]);
            }
        }
        System.out.println(theQueue.peekMid());
    }
    
}
class Queue{
        private int maxSize;
        private String[] queArray;
        private int front;
        private int rear;
        private int nItems;
        public Queue(int s) { // constructor
            maxSize = s;
            queArray = new String[maxSize];
            front = 0;
            rear = -1;
            nItems = 0;
        }
        public boolean insert(String j) { // put item at rear of queue
                if(isFull()) return false; //don’t remove if full
                if(rear == maxSize-1) // deal with wraparound
                rear = -1;
                rear++;
                queArray[rear] = j; // increment rear and insert
                nItems++; // one more item
              
                return true; //successfully inserted
} 
        public String remove() { // take item from front of queue
                if(isEmpty()) return null; //don’t remove if empty
                String temp = queArray[front];// get value and incr front
                front++;
                if(front == maxSize){ // deal with wraparound
                    front = 0;}
                nItems--; // one less item
                return temp;
            }
        public String peekFront(){ // peek at front of queue
        return queArray[front];
        } 
        public boolean isEmpty() { // true if queue is empty
        return (nItems==0);
        } 
        public boolean isFull() { // true if queue is full
        return (nItems==maxSize);
        } 
        public int size() { // number of items in queue
        return nItems;
        } 
        public String peekMid(){
            String[] ordered = new String[nItems];
            int check = front;
            for(int i = 0; i<nItems; i++){
                ordered[i] = queArray[check];
                System.out.println(ordered[i]);  
                check++;
            }
            return(nItems % 2 == 0) ? ordered[--nItems/2] : ordered[nItems/2];
        }
    }
