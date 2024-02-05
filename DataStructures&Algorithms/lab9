public class lab9
{
    public static void main (String [] args){
        System.out.println(part1(68));
        System.out.println(part2(7f,1.3f,100f)+100);
        System.out.println(100*(1-part3(40)));
    }
    public static long part1(int n){
        if(n == 1){
            return 2;
        }
        else{
            return 4*(part1(n-1))-3*n;
        }
    }
    public static float part2(float y, float i, float bal) {
        if(y == 1) {
           return bal*i*0.01f;
        }
        else{
            float newBal = bal*i*0.01f;
            return newBal + part2(--y, i, bal + newBal);
        }
    }
    public static float part3(int n){
        if(n ==1)
        {return 0;
        }
        else if(n==2){
            return 364/365f;
        }
        else{
            return part3(n-1)*(366-n)/365f;
        }
    }
}
