<script setup lang="ts">
import { usePersonStore } from '@/stores/people';

import AddPersonButton from '../components/AddPersonButton.vue';
import PersonRecorder from '../components/PersonRecorder.vue';

const people = usePersonStore();
</script>

<template>
  <main class="flex gap-16 items-start flex-wrap">
    <PersonRecorder
      v-for="(person, index) in people.people"
      :key="index"
      :person="person"
    />

    <AddPersonButton />
  </main>
</template>
