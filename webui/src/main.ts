import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faCirclePlay,
  faCirclePlus,
  faCircleStop,
  faCircleXmark,
  faUserCircle,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { initializeApp } from 'firebase/app';
import { createPinia } from 'pinia';
import { createApp } from 'vue';

import router from './router';

import './index.css';

import App from './App.vue';
import { faGoogle } from '@fortawesome/free-brands-svg-icons';
import { browserLocalPersistence, getAuth, setPersistence } from '@firebase/auth';

var firebaseConfig = {
  apiKey: 'AIzaSyC4piMlw29usc_TebF_EBKkm5fTfn34-Ns',
  authDomain: 'where-has-my-time-gone.firebaseapp.com',
  projectId: 'where-has-my-time-gone',
  storageBucket: 'where-has-my-time-gone.appspot.com',
  messagingSenderId: '425698946879',
  appId: '1:425698946879:web:00b4a67296140cfe7b2d8a',
};

initializeApp(firebaseConfig);

const auth = getAuth();
setPersistence(auth, browserLocalPersistence);

library.add(
  faUserCircle,
  faCirclePlus,
  faCirclePlay,
  faCircleStop,
  faCircleXmark,
  faGoogle
);
const app = createApp(App).component('font-awesome-icon', FontAwesomeIcon);

app.use(createPinia());
app.use(router);

app.mount('#app');
