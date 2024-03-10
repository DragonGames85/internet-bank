import axios from 'axios';
import { User } from './types';
import { setURL } from '../config';

export class usersApi {
    public getAll() {
        setURL(7228);
        return axios.get<User[]>('/user/all').then(res => res.data);
    }
    public post(user: { login: string; password: string; name: string; role: 'Employee' | 'Customer' }) {
        setURL(7228);
        return axios.post('/auth/register', user).then(res => res.data);
    }
    public delete(userId: string) {
        setURL(7228);
        return axios.delete(`/user/${userId}`).then(res => res.data);
    }
    public ban(userId: string) {
        setURL(7228);
        return axios.post(`/user/${userId}/ban`).then(res => res.data);
    }
}
