import { Account } from './events/Account'
import { App} from './App'

export function initialize() {
  console.log('Initialize main ODX library')
}

exports.Account = Account
exports.App = App
