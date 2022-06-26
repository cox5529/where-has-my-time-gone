<script setup lang="ts">
import PersonAnalytics from '../components/PersonAnalytics.vue';
import { computed } from 'vue';

import { usePersonStore } from '@/stores/people';

const personStore = usePersonStore();
const people = computed(() => personStore.people);
await personStore.load();
</script>

<template>
  <div class="flex flex-col gap-8">
    <PersonAnalytics
      v-for="person in people"
      :key="person.id"
      :person="person"
    ></PersonAnalytics>
  </div>
</template>
