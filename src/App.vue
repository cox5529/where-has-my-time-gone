<script setup lang="ts">
import { RouterView } from 'vue-router';

import NewPersonModal from './components/modals/NewPersonModal.vue';
</script>

<template>
  <div class="bg-slate-900 text-white min-h-screen p-8">
    <RouterView />
  </div>

  <NewPersonModal />
</template>
