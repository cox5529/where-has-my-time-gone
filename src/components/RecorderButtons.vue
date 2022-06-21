<script setup lang="ts">
import { padStart } from 'lodash';
import { computed, onUnmounted, ref } from 'vue';

const emit = defineEmits<{
  (e: 'duration-posted', start: string, duration: number): void;
}>();

const start = ref<number | null>(null);
const duration = ref<number>(0);
const durationString = computed(() => {
  const hours = Math.floor(duration.value / 3600);
  const minutes = Math.floor(duration.value / 60);
  const seconds = duration.value % 60;

  const minutesString = padStart(`${minutes}`, 2, '0');

  const secondsString = padStart(`${seconds}`, 2, '0');
  return `${hours}:${minutesString}:${secondsString}`;
});

let intervalHandle: number;

function onStart(): void {
  start.value = new Date().getTime();
  duration.value = 0;

  intervalHandle = setInterval(() => {
    const current = new Date().getTime();
    duration.value = Math.floor((current - (start.value as number)) / 1000);
  }, 250) as unknown as number;
}

function onStop(): void {
  clearInterval(intervalHandle);

  if (start.value) {
    emit(
      'duration-posted',
      new Date(start.value).toISOString(),
      duration.value
    );
  }

  start.value = null;
}

onUnmounted(() => clearInterval(intervalHandle));
</script>

<template>
  <div class="flex gap-4 items-center">
    <button @click="onStart" :disabled="!!start">
      <font-awesome-icon
        class="w-8 h-8"
        :class="{ 'text-green-700': start }"
        icon="fa-solid fa-circle-play"
      />
    </button>
    <h5
      class="bg-black p-2 rounded-md font-mono"
      :class="{ 'text-green-300': start, 'text-red-300': !start }"
    >
      {{ durationString }}
    </h5>
    <button @click="onStop" :disabled="!start">
      <font-awesome-icon
        class="w-8 h-8"
        :class="{ 'text-red-700': !start }"
        icon="fa-solid fa-circle-stop"
      />
    </button>
  </div>
</template>
