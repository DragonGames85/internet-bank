import axios from 'axios';
import { authAppUrl, setURL } from '../config';

export class authApi {
    public setTheme(isLightTheme: boolean) {
        setURL(authAppUrl);
        return axios.post('/Settings/config', { isLightTheme });
    }
    public hiddenAccounts() {
        setURL(authAppUrl);
        return axios.get('/Settings/hideAccount', {}).then(res=> res.data);
    }
    public hideAccount(accountId: string) {
        setURL(authAppUrl);
        return axios.post(`/Settings/hideAccount?accountId=${accountId}`);
    }
    public showAccount(accountId: string) {
        setURL(authAppUrl);
        return axios.delete(`/Settings/hideAccount?accountId=${accountId}`);
    }
}
