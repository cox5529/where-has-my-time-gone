<script setup lang="ts">
import { useAuthenticationStore } from '@/stores/authentication';
import { computed } from 'vue';

import GoogleSignInButton from './GoogleSignInButton.vue';

const authStore = useAuthenticationStore();

const email = computed(() => authStore.user?.email);
</script>

<template>
  <div
    class="h-16 bg-slate-700 flex items-center text-white gap-8 justify-end px-4"
  >
    <p v-if="email">Hello, {{ email }}</p>
    <GoogleSignInButton />
  </div>
</template>
