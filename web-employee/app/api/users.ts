import axios from 'axios';
import { User } from './types';

export class usersApi {
    public getAll() {
        return axios.get('/user/all').then(res => res.data);
    }
    public post(user: Omit<User, 'id'> & Partial<User>) {
        return axios.post('/users', user).then(res => res.data);
    }
    public put(id: string, user: Omit<User, 'id' | 'role'> & Partial<User>) {
        return axios.put(`/user/edit/${id}`, user).then(res => res.data);
    }
    public delete(id: string) {
        return axios.delete(`/users/${id}`).then(res => res.data);
    }
    public ban(id: string, user: Omit<User, 'id' | 'role'> & Partial<User>) {
        return axios.put(`/users/${id}`, user).then(res => res.data);
    }
}
