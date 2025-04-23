import { CommonLib } from '../Common'

export class AccountService {

    common: CommonLib
    context: Xrm.Events.EventContext

    constructor(context: Xrm.Events.EventContext) {
        this.common = new CommonLib(context)
        this.context = context; 
    }

    public sayHello() : void
    {
        var name = this.context.getFormContext().getAttribute("name"); 
        if(name){
            console.log("Hello from " + name.getValue()); 
        }
        else{
            console.log("Hello from John Doe"); 
        }  
    }

    public switchFormAsync() : Promise<string>
    {
        return new Promise((resolve, reject) => {
            let formItem = this.context.getFormContext().ui.formSelector.items.get(1); 
            console.log(formItem);
            formItem.navigate(); 
            resolve("Form switched"); 
        }); 
    }

    public showConfirmationAsync(): Promise<Xrm.Navigation.ConfirmResult>
    {
        let saveContext = this.context as Xrm.Events.SaveEventContext; 
        let eventArgs : any = saveContext.getEventArgs();
        eventArgs.disableAsyncTimeout(); //disable default timeout 

        return new Promise((resolve, reject) => {
                if (eventArgs.getSaveMode() == 70){ //prevent autosave
                    eventArgs.preventDefault();
                    return;
                }; 
               
               Xrm.Navigation.openConfirmDialog({ 
                       text:"Would you like to save 2?", 
                       title:"Save confirmation" 
                    }, { 
                        height: 200, 
                        width: 450 })
                .then((success) =>{
                    resolve(success);
                    if (success.confirmed){
                        console.log("Dialog closed using OK button.");
                    }
                    else{
                        console.log("Dialog closed using Cancel button or X.");
                        eventArgs.preventDefault(); //stop saving
                    }
                });
        });
    }
}