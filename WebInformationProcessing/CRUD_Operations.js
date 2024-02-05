//auther Keith Considine
//Student Number: 20448132

//Worked for me in InteliJ and Replit online IDE
//Worked for me in Microsoft edge but should work in any browser that can use Replit and MongodbAtlas

//would recommend commenting out method calls for methods
// that you dont want to run as by default ALL CRUD methods will run on each collection


const { MongoClient } = require("mongodb");
// Replace the uri string with your MongoDB deployment's connection string.
const uri = "mongodb+srv://u210149:bX6aYC4sCJuxplZQ@cluster0.mdvtm.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
const client = new MongoClient(uri);
const titles = ["Mr","Mrs","Dr","Miss","Ms","Mz"];
const LastNames = ["Gill","Jones","Smith","Doe","Considine","Jane","Black","White","Brown","Fallon","Mallon","Doyle","Boyle","Grant"];
const FirstNames = ["Steve","Allan","Jerry","Jack","Keith","Jill","Bill","Will","Rick","John","Tim","Tom","Tammy","Sean","Emma","Tilly","Jake","James","Clare","Sarah","Jess"];
const Address1s = ["Ballygar", "BallyCloon","Ballnakill","Newtown","Cloncurry","Ballyvoneen","Ballyslough","BallyKill"];
const Towns = ["Mullingar","Ennis","Longford","Kilcock","Enfield","Athlone","Shannon","Mallow","Ballina","Kilarney","Dundalk"];
const Counties = ["Derry","Kildare","Mayo","Dublin","Carlow","Clare","Cork","Galway","Meath","Roscommon","Kerry"];
const manufactuerers = ["Apple","Samsung","Nokia","Sony","huawei"];
const models = ["Iphone11","Iphone12","Iphone13","GalaxyA20","Xperia5","P50","GalaxyS10+","GalaxyS20","GalaxyA52","G11"];
const prices = ["500.00","100.00","200.00","300.00","490.00","400.00","350.00","600.00","540.00"]; 
//the above arrays were used to insert random data into my database during inserts and updates
async function run() {
    try {
        await client.connect().then(()=>{console.log("connected")});
        //these three methods were used to create my collections with validations
     // await createPersonalDetails(client);
      //  await createItemDetails(client);
       // await createOrderDetails(client);
        const PersonalDetails = client.db("myFirstDatabase").collection("PersonalDetails");
        const ItemDetails = client.db("myFirstDatabase").collection("ItemDetails");
        const OrderDetails = client.db("myFirstDatabase").collection("OrderDetails");
      //await retrieveAllData(PersonalDetails,ItemDetails,OrderDetails); //this method was used to print all item and customer data so i could put it in a text file
        await insertCustomer(PersonalDetails, { //insert customer with specified details
            Title: "Mr",
            FirstName: "Jeryy",
            LastName: "Doe",
            Mobile: "0878534346",
            EmailAddress: "thejer86765@gmail.com",
            HomeAddress:{
                AddressLine1: "Newtown",
                Town: "Kilcock",
                County: "Kildare",
                Eircode: "A68YT49"
            },
            ShippingAddress:{
                AddressLine1: "Newtown",
                Town: "Kilcock",
                County: "Kildare",
                Eircode: "A68YT49"
            },
         })
      
        await insertItem(ItemDetails, { //insert item with specified details
            Manufacturer: "Apple",
            Model: "Iphone12",
            Price: "350.00"
        })
       await insertOrder(OrderDetails,ItemDetails,PersonalDetails); //insert order with random customer and item
      await findCustomer(PersonalDetails); //read random customer
       await findItem(ItemDetails);//read random item
      await findOrder(OrderDetails);//read random order
        await UpdateCustomer(PersonalDetails,{ //update random customer with random attributes from above arrays
                Title: titles[Math.floor(Math.random() * titles.length)],
                FirstName: FirstNames[Math.floor(Math.random() * FirstNames.length)],
                LastName: LastNames[Math.floor(Math.random() * LastNames.length)],
                HomeAddress:{
                  AddressLine1: Address1s[Math.floor(Math.random() * Address1s.length)],
                  Town: Towns[Math.floor(Math.random() * Towns.length)],
                  County: Counties[Math.floor(Math.random() * Counties.length)],
                }
        })
      await UpdateItem(ItemDetails,{ //update random item with random attributes from above arrays
                Manufacturer: manufactuerers[Math.floor(Math.random() * manufactuerers.length)],
                Model: models[Math.floor(Math.random() * models.length)],
                Price: prices[Math.floor(Math.random() * prices.length)]
      })
      await UpdateOrder(OrderDetails,ItemDetails) //update random order with new item
      await deleteCustomer(PersonalDetails);//delete random customer
      await deleteItem(ItemDetails);//delete random item
      await deleteOrder(OrderDetails);//delete random order
    } finally {
        // Ensures that the client will close when you finish/error
        await client.close();
    }
}
async function insertCustomer(PersonalDetails, newPersonal){//method to insert new customer
    const result = await PersonalDetails.insertOne(newPersonal);//insert customer
    console.log(`New Person inserted with the id: ${result.insertedId}`);//print new customers id
}
async function insertItem(ItemDetails, newItem){//insert item method
    const result = await ItemDetails.insertOne(newItem);//insert new item
    console.log(`New Item inserted with the id: ${result.insertedId}`);//print new items id
}
async function insertOrder(OrderDetails,ItemDetails,PersonalDetails){ //method to insert order, to do this i randomly select a customer and item and create an order between therm
  var cursor = await ItemDetails.find({});//read all items
  var array = await cursor.toArray();//convert to array
  const Item = array[Math.floor(Math.random() * array.length)];//pick random
  cursor = await PersonalDetails.find({});//read all customers
  array = await cursor.toArray();//convert to array
  const Customer = array[Math.floor(Math.random() * array.length)];//pick random
  const newOrder = {//new order = Customer + item
    Customer,
    Item
  }
  const result = await OrderDetails.insertOne(newOrder); //insert new order
  console.log(`New Order inserted with the id: ${result.insertedId}`);//print new orders ID
}
async function findCustomer(PersonalDetails){//method to read customer
    const cursor = await PersonalDetails.find({});//read all customers
    const result = await cursor.toArray();//convert to array
    if (result) {
        console.log(result[Math.floor(Math.random() * result.length)]);//print a random customer
    }
    else {
        console.log("no search result found");
    }
}
async function findItem(ItemDetails){//item to read item
    const cursor = await ItemDetails.find({});//read all items
    const result = await cursor.toArray();//convert to array
    if (result) {
        console.log(result[Math.floor(Math.random() * result.length)]);//print random item
    }
    else {
        console.log("no search result found");
    }
}
async function findOrder(OrderDetails){//method to read order
    const cursor = await OrderDetails.find({});//read all orders
    const result = await cursor.toArray();//convert to array
    if (result) {
        console.log(result[Math.floor(Math.random() * result.length)]);//print random order
    }
    else {
        console.log("no search result found");
    }
}
async function UpdateCustomer(PersonalDetails,updatedcustomer){//method to update customer
    const cursor = await PersonalDetails.find({});//retrieve all customers
    const customers = await cursor.toArray();//convert to array
    const selected = customers[Math.floor(Math.random() * customers.length)];//select one at random
    const input = selected._id;//get its id
    const result = await PersonalDetails.updateOne({_id: input}, {$set: updatedcustomer })//update customer with selected id with new details
  console.log("updated person");
}
  async function UpdateItem(ItemDetails,updateditem){//method to update item
    const cursor = await ItemDetails.find({});//read all itemns
    const array = await cursor.toArray();//convert to array
    const selected = array[Math.floor(Math.random() * array.length)];//select random
    const input = selected._id;//take id 
    const result = await ItemDetails.updateOne({_id: input}, {$set: updateditem }) //update item with selected id
  console.log("updated item");
}
async function UpdateOrder(OrderDetails,ItemDetails){//method to update order
  var cursor = await OrderDetails.find({});//read all orders
  var array = await cursor.toArray();//convert to array
  const order = array[Math.floor(Math.random() * array.length)];//take one at random
  cursor = await ItemDetails.find({});//take all items
  array = await cursor.toArray();//convert to array
  const Customer = order.Customer;//take current customer data from selected order
  const Item = array[Math.floor(Math.random() * array.length)];//take an item at random
  const result = await ItemDetails.updateOne({order}, {$set: { 
    //update the selected order so that the customer data remains the same but the item is changed to a random item
    Customer,
    Item                                                
  } })
  console.log("updated order");
}
async function deleteCustomer(PersonalDetails){//method for deleting personal data
    const cursor = await PersonalDetails.find({});//read all customers
    const array = await cursor.toArray();//covert to array
    const selected = array[Math.floor(Math.random() * array.length)]//select random 
    const result = await PersonalDetails.deleteOne(selected);//delete selected
    console.log("deleted Customer");
}
async function deleteItem(ItemDetails){//method for deleting item
    const cursor = await ItemDetails.find({});//read all items
    const array = await cursor.toArray();//convert to array
    const selected = array[Math.floor(Math.random() * array.length)]//selected one at random
    const result = await ItemDetails.deleteOne(selected);//delete selected
    console.log("deleted Item");
}
async function deleteOrder(OrderDetails){ //method for deleting order
    const cursor = await OrderDetails.find({}); //read all orders
    const array = await cursor.toArray();//convert to array
    const selected = array[Math.floor(Math.random() * array.length)]//select one at random
    const result = await OrderDetails.deleteOne(selected);//delete selected
    console.log("deleted Order");
}
async function retrieveAllData(PersonalDetails,ItemDetails){// method to print out all data in array(Customers and Items since orders is just duplicate data) i then put this in a .txt file
    const details = [PersonalDetails,ItemDetails];
    for(i = 0; i<details.length; i++){
      const cursor = await details[i].find({});
      const result = await cursor.toArray();
      if (result) {
          console.log(result);
      }
      else {
          console.log("no search result found");
      }
    }
}
//I used these three methods to create my collections with validations so i could have required fields, sometimes validator erros happen despite input matching validation, if you wait and try again it works, i have no idea why that happens but it does
async function createPersonalDetails(client){
    await client.db(process.env.Assignment5).createCollection("PersonalDetails", { //method for creating personal details collection with validations
        capped: false,
        validator: {
            $jsonSchema: {
                bsonType: "object",
                title: "PersonalDetails",
                additionalProperties: false,
                properties: {
                    _id: {
                        bsonType: "objectId",
                    },
                    Title:{
                        enum: [
                            "Mr",
                            "Mrs",
                            "Mx",
                            "Ms",
                            "Miss",
                            "Dr"
                        ]
                    },
                    FirstName: {
                        bsonType: "string",
                    },
                    LastName: {
                        bsonType: "string",
                    },
                    Mobile:{
                        bsonType: "string",
                    },
                    EmailAddress:{
                        bsonType: "string",
                    },
                    HomeAddress:{
                        bsonType: "object",
                        title: "HomeAddress",
                        additionalProperties: false,
                        properties: {
                            AddressLine1:{
                                bsonType: "string",
                            },
                            AddressLine2:{
                                bsonType: "string",
                            },
                            Town:{
                                bsonType: "string",
                            },
                            County:{
                                bsonType: "string",
                            },
                            Eircode:{
                                bsonType: "string",
                            },
                        },
                        required: ["AddressLine1","Town","County"],
                    },
                    ShippingAddress:{
                        bsonType: "object",
                        title: "ShippingAddress",
                        additionalProperties: false,
                        properties: {
                            AddressLine1:{
                                bsonType: "string",
                            },
                            AddressLine2:{
                                bsonType: "string",
                            },
                            Town:{
                                bsonType: "string",
                            },
                            County:{
                                bsonType: "string",
                            },
                            Eircode:{
                                bsonType: "string",
                            },
                        },
                        required: ["AddressLine1","Town","County"],
                    },
                },
                required: ["FirstName", "LastName", "Mobile","EmailAddress","HomeAddress","ShippingAddress"],
            },
        },
        validationLevel: "strict",
        validationAction: "error",
    });
}//this function creates empty PersonalDetails collection with validations
async function createItemDetails(client){
    await client.db(process.env.Assignment5).createCollection("ItemDetails", { //method for creating Item details collection with validations
        capped: false,
        validator: {
            $jsonSchema: {
                bsonType: "object",
                title: "ItemDetails",
                additionalProperties: false,
                properties: {
                    _id: {
                        bsonType: "objectId",
                    },
                    Manufacturer: {
                        bsonType: "string",
                    },
                    Model:{
                        bsonType: "string",
                    },
                    Price:{
                        bsonType: "string",
                    },
                },
                required: ["Manufacturer","Model","Price"]
            },
        },
        validationLevel: "strict",
        validationAction: "error",
    });
}//this function creates empty ItemDetails collection with validations
async function createOrderDetails(client) {
    await client.db(process.env.Assignment5).createCollection("OrderDetails", { //method for creating Order details collection with validations
        capped: false,
        validator: {
            $jsonSchema: {
                bsonType: "object",
                title: "OrderDetails",
                additionalProperties: false,
                properties: {
                    _id: {
                        bsonType: "objectId",
                    },
                    Customer: {
                        bsonType: "object",
                        title: "Customer",
                       additionalProperties: true,
                    },
                    Item: {
                        bsonType: "object",
                        title: "Item",
                      additionalProperties: true,
                    },
                    Item2: {
                        bsonType: "object",
                        title: "Item2",
                      additionalProperties: true,
                    },
                },
                required: ["Customer", "Item"]
            }
        },
        validationLevel: "strict",
        validationAction: "error",
    });
}//this function creates empty OrderDetails collection with validations
run().catch(console.dir);

//I split my database into three collections, Personal details, Item Details and Order Details
//In personal details all of the fields are strings, except for the addresses both of which are objects with the details
//inside as strings. Item details all fields are strings including price, I was getting validator errors when trying to have it 
//as a double so I decided to just have it as a string.
//order details consists of 2 objects, the customer and the item. When creating a order I used .find on 
//both the item and personal details and inputted the data into orderdetails
