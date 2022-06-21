import { library } from '@fortawesome/fontawesome-svg-core';
import { faCirclePlus, faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { createPinia } from 'pinia';
import { createApp } from 'vue';

import router from './router';

import './index.css';

import App from './App.vue';

library.add(faUserCircle, faCirclePlus);
const app = createApp(App).component('font-awesome-icon', FontAwesomeIcon);

app.use(createPinia());
app.use(router);

app.mount('#app');
