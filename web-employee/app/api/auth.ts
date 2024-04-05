import axios from 'axios';
import { authAppUrl, setURL } from '../config';

export class authApi {
    public register(user: { login: string; name: string; password: string }, role: 'Customer' | 'Employee') {
        setURL(authAppUrl);
        return axios.post<{ token: string; userId: string }>('/auth/register', { ...user, role }).then(res => res.data);
    }
    public setTheme(isLightTheme: boolean) {
        setURL(authAppUrl);
        return axios.post('/Settings/config', { isLightTheme }).then(res => res.data);
    }
}
