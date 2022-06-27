<script setup lang="ts">
import { onUnmounted, ref } from 'vue';

import moment from 'moment';

const props = defineProps<{ start: string; height: number }>();

const totalSeconds = 24 * 60 * 60;

const offset = ref(0);

updateOffset();
const interval = setInterval(updateOffset, 1000);

function updateOffset() {
  const newOffset = convertDateToOffset(moment());
  offset.value = newOffset;
}

function convertDateToOffset(date: moment.Moment): number {
  return (
    ((date.diff(moment(props.start)) / 1000) * props.height) / totalSeconds
  );
}

onUnmounted(() => {
  clearInterval(interval);
});
</script>

<template>
  <div
    class="border-t-2 absolute w-full border-red-500"
    :style="{ top: `${offset}px` }"
  ></div>
</template>
