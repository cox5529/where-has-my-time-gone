import HomeView from '../views/HomeView.vue';

import {
  type NavigationGuardWithThis,
  createRouter,
  createWebHistory,
} from 'vue-router';

import { useAuthenticationStore } from './../stores/authentication';

const isLoggedInGuard: NavigationGuardWithThis<any> = async () => {
  const authStore = useAuthenticationStore();
  if (authStore.isLoginStateKnown) {
    return !!authStore.isLoggedIn;
  }

  const promise = new Promise<any>((resolve) => {
    const cancelSubscription = authStore.$subscribe((mutation, state) => {
      if (state.isLoggedIn !== undefined && state.isLoggedIn) {
        cancelSubscription();
        resolve(state.isLoggedIn);
      } else if (state.isLoggedIn !== undefined) {
        cancelSubscription();
        resolve({ name: 'home' });
      }
    });
  });

  return await promise;
};

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/analytics',
      name: 'analytics',
      component: () => import('../views/AnalyticsView.vue'),
    },
    {
      path: '/huddles',
      name: 'huddles',
      component: () => import('../views/HuddlesView.vue'),
      beforeEnter: isLoggedInGuard,
    },
  ],
});

export default router;
