<script setup lang="ts">
import { usePersonStore } from '@/stores/people';
import { computed } from 'vue';

import PersonAnalytics from '../components/PersonAnalytics.vue';

const personStore = usePersonStore();
const people = computed(() => personStore.people);
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
