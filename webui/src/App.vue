<script setup lang="ts">
import { getAuth } from 'firebase/auth';
import { RouterView } from 'vue-router';

import { useAuthenticationStore } from './stores/authentication';
import { usePersonStore } from './stores/people';

import TopNavbar from './components/TopNavbar.vue';
import NewPersonModal from './components/modals/NewPersonModal.vue';

const auth = getAuth();

const authStore = useAuthenticationStore();
const personStore = usePersonStore();
auth.onAuthStateChanged(async (user) => {
  authStore.user = user;

  await authStore.request('/api/profile/me');
  await personStore.load();
});
</script>

<template>
  <div class="bg-slate-900 min-h-screen">
    <TopNavbar />
    <div class="text-white p-8">
      <RouterView />
    </div>
  </div>

  <NewPersonModal />
</template>
