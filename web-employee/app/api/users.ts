import axios from 'axios';
import { User } from './types';
import { authAppUrl, setURL } from '../config';

export class usersApi {
    public getAll() {
        setURL(authAppUrl);
        return axios.get<User[]>('/user/all').then(res => res.data);
    }
    public post(user: { login: string; password: string; name: string; role: 'Employee' | 'Customer' }) {
        setURL(authAppUrl);
        return axios.post('/auth/register', user).then(res => res.data);
    }
    public delete(userId: string) {
        setURL(authAppUrl);
        return axios.delete(`/user/${userId}`).then(res => res.data);
    }
    public ban(userId: string) {
        setURL(authAppUrl);
        return axios.post(`/user/${userId}/ban`).then(res => res.data);
    }
}
