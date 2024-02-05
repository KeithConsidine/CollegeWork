public class Main
{
    public static void main(String [] args)
    {
        String [] wordArr = {"one", "two", "three", "four", "run", "apple", "banana", "correct", "aardvark"};
        sortAlph(wordArr);
        for(String word : wordArr){
            System.out.print(word + " ");
        }
        System.out.println();
        sortScrab(wordArr);
        for(String word : wordArr){
            System.out.print(word+" ");
        }
    }
    public static void sortAlph(String[] arr){
        for(int outer = 1; outer < arr.length; outer++){
            String temp = arr[outer];
            int inner = outer-1;
            while(inner >= 0 && arr[inner].compareTo(temp) > 0){
                arr[inner+1] = arr[inner];
                inner -= 1;
            }
            arr[inner+1] = temp;
        }
    }
    public static void sortScrab(String[] arr){
        for(int outer = 1; outer < arr.length; outer++){
            String temp = arr[outer];
            int inner = outer-1;
            while(inner >= 0 && check(arr[inner], temp))
            {
                arr[inner+1] = arr[inner];
                inner -= 1;
            }
            arr[inner+1] = temp;
        }
    }
    public  static boolean check(String first, String second){
        int[] values = {1,3,3,2,1,4,2,4,1,8,5,1,3,1,1,3,10,1,1,1,1,4,4,8,4,10};
        int fScrScore = 0, sScrScore = 0;
        for(int i = 0; i < first.length(); i++){
            fScrScore += values[((int)first.charAt(i)) -97];
        }
        for(int i = 0; i< second.length(); i++){
            sScrScore += values[((int) second.charAt(i))-97];
        }
        return (fScrScore > sScrScore);
    }
}
