import { AccountService } from '../services/AccountService'

export class Account{

    public static async onLoad(context : Xrm.Events.EventContext) {
        const accountService = new AccountService(context)
        accountService.sayHello(); 
    }

    //Sample handler for async load event
    //Swiches forms without waiting for default (or last form) to be loaded
    //Async OnLoad setting must be enabled for the MDA to make it work 
    public static async onLoadAsync(context : Xrm.Events.EventContext) : Promise<string>{
        const accountService = new AccountService(context)
        return accountService.switchFormAsync(); 
    }

    //Sample handler for async save event
    //Shows save confirmation window and waits for user decision 
    //Async OnSave setting must be enabled for the MDA to make it work
    public static async onSaveAsync(context : Xrm.Events.SaveEventContext) : Promise<Xrm.Navigation.ConfirmResult> {
        const accountService = new AccountService(context)
        return accountService.showConfirmationAsync(); 
    }
}
