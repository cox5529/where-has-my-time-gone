<script setup lang="ts">
import { getAuth } from 'firebase/auth';
import { RouterView } from 'vue-router';

import { useAuthenticationStore } from './stores/authentication';

import TopNavbar from './components/TopNavbar.vue';
import NewPersonModal from './components/modals/NewPersonModal.vue';

const auth = getAuth();

const authStore = useAuthenticationStore();
auth.onAuthStateChanged((user) => (authStore.user = user));
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
