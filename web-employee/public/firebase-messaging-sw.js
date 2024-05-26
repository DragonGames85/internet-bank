// Import and configure the Firebase SDK
importScripts('https://www.gstatic.com/firebasejs/9.6.1/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.6.1/firebase-messaging-compat.js');

// Your web app's Firebase configuration
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
firebase.initializeApp(firebaseConfig);

// Retrieve Firebase Messaging object.
const messaging = firebase.messaging();

// Handle background messages
messaging.onBackgroundMessage(payload => {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    const notificationTitle = payload.notification.title;
    const notificationOptions = {
        body: payload.notification.body,
        icon: '/firebase-logo.png',
    };

    self.registration.showNotification(notificationTitle, notificationOptions);
});
