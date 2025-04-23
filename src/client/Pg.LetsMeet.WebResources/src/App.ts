export class App {

    public static async onLoad(context: Xrm.Events.EventContext) {

        console.log("ON LOAD STARTED");

        var settingValue = Xrm.Utility.getGlobalContext().getCurrentAppSetting("pg_welcomescreen");

        if(settingValue === true){ 
            
            console.log("settingValue === true");

            var pageInput: Xrm.Navigation.CustomPage = {
                pageType: "custom",
                name: "pg_letsmeetwelcomepage_f99a3",
            };
            var navigationOptions: Xrm.Navigation.NavigationOptions = {
                target: 2,
                position: 1,
                width: { value: 800, unit: "px" },
                height: { value: 600, unit: "px" },
                title: "Welcome!"
            };
            Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                function(){
                    console.log("OPERATION COMPLETED SUCESSFULLY"); 
                }, 
                function(err){
                    console.log("ERROR REPORTED!"); 
                    console.log(err);  
                }
            )
        }
        else{
            console.log("settingValue !== true");
        }

 
        console.log("ON LOAD COMPLETED");
    }

}
