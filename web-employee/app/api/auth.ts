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
    public options(userId: string, options: { theme: 'light' | 'dark'; hidden_accounts: string[] }) {
        setURL(authAppUrl);
        return axios.put('/auth/options', { userId, options }).then(res => res.data);
    }
}
