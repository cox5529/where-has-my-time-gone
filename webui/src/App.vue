<script setup lang="ts">
import TopNavbar from './components/TopNavbar.vue';
import NewPersonModal from './components/modals/NewPersonModal.vue';

import { getAuth } from 'firebase/auth';
import { RouterView } from 'vue-router';

import { useAuthenticationStore } from './stores/authentication';

const auth = getAuth();

const authStore = useAuthenticationStore();

auth.onAuthStateChanged(async (user) => {
  authStore.user = user;
  authStore.isLoggedIn = !!user;

  if (user) {
    await authStore.request('/api/profile/me');
  }
});
</script>

<template>
  <div class="bg-slate-900 min-h-screen">
    <TopNavbar />
    <div class="text-white p-8" v-if="authStore.isLoggedIn !== undefined">
      <Suspense>
        <RouterView />

        <template #fallback> Loading...</template>
      </Suspense>
    </div>
  </div>

  <NewPersonModal />
</template>
