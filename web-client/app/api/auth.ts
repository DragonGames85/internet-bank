import axios from 'axios';
import { authAppUrl, setURL } from '../config';

export class authApi {
    public login(user?: { login: string; password: string }) {
        setURL(authAppUrl);
        return axios.post<{ token: string; userId: string; id: string }>('/auth/login', user).then(res => res.data);
    }
    public register(user: { login: string; name: string; password: string }) {
        setURL(authAppUrl);
        return axios
            .post<{ token: string; userId: string }>('/auth/register', { ...user, role: 'Customer' })
            .then(res => res.data);
    }
    public setTheme(isLightTheme: boolean) {
        setURL(authAppUrl);
        return axios.post('/Settings/config', { isLightTheme }).then(res => res.data);
    }
    public hiddenAccounts() {
        setURL(authAppUrl);
        return axios.get('/Settings/hideAccount', {}).then(res => res.data);
    }
    public hideAccount(accountId: string) {
        setURL(authAppUrl);
        return axios.post(`/Settings/hideAccount?accountId=${accountId}`).then(res => res.data);
    }
    public showAccount(accountId: string) {
        setURL(authAppUrl);
        return axios.delete(`/Settings/hideAccount?accountId=${accountId}`).then(res => res.data);
    }
}
