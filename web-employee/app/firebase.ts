// Import the functions you need from the SDKs you need

import { initializeApp } from 'firebase/app';
import { getAnalytics } from 'firebase/analytics';
import { getMessaging } from 'firebase/messaging';
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: 'AIzaSyBqkwbKrq8dJPovKt6zs605r1B5WaIE5hw',
    authDomain: 'internet-bank-notifications.firebaseapp.com',
    projectId: 'internet-bank-notifications',
    storageBucket: 'internet-bank-notifications.appspot.com',
    messagingSenderId: '151897464505',
    appId: '1:151897464505:web:9f86fd706ac8a3acdd0568',
    measurementId: 'G-7ENT5V5KRF',
};

// Initialize Firebase
export const app = initializeApp(firebaseConfig);
export const messaging = getMessaging(app);
