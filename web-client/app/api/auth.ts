import axios from 'axios';
import { setURL } from '../config';

export class authApi {
    public login(user: { login: string; password: string }) {
        setURL(7228);
        return axios.post<{ token: string; userId: string }>('/auth/login', user).then(res => res.data);
    }
    public register(user: { login: string; name: string; password: string }) {
        setURL(7228);
        return axios
            .post<{ token: string; userId: string }>('/auth/register', { ...user, role: 'Customer' })
            .then(res => res.data);
    }
}
