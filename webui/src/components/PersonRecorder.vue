<script setup lang="ts">
import type { Person } from '@/models/person';
import { usePersonStore } from '@/stores/people';

import ProfileImage from './ProfileImage.vue';
import RecorderButtons from './RecorderButtons.vue';

const props = defineProps<{
  person: Person;
}>();

const people = usePersonStore();

function onDurationPosted(start: string, duration: number): void {
  people.addUsage(props.person.id, { start, duration, type: 'Manual' });
}
</script>

<template>
  <div class="flex flex-col items-center gap-4">
    <ProfileImage :url="props.person.image" :name="props.person.name" />
    <h3 class="text-3xl">{{ props.person.name }}</h3>
    <RecorderButtons @duration-posted="onDurationPosted" />
  </div>
</template>
