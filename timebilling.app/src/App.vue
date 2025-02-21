<script setup>
  import { ref, reactive, computed, onMounted } from "vue";
  import { formatMoney } from "./formatters";
  import axios from "axios";
  const name = ref("Brian");
  const kodo = ref("kodo");
  const bills = reactive([
    //  {
    //    "hoursWorked": 3.0,
    //    "rate": 225.00,
    //    "date": "2023-05-05",
    //    "work": "I did a thing...",
    //    "customerId": 1,
    //    "employeeId": 1
    //  },
    //{
    //    "hoursWorked": 2.0,
    //    "rate": 800.00,
    //    "date": "2023-05-01",
    //    "work": "she did a thing...",
    //    "customerId": 2,
    //    "employeeId": 2
    //  },
    //{
    //    "hoursWorked": 5.0,
    //    "rate": 150.00,
    //    "date": "2023-05-03",
    //    "work": "he did a thing...",
    //    "customerId": 3,
    //    "employeeId": 3
    //  }
  ]);

  //api call
  onMounted(async () => {
    const result = await axios("/api/customers/2/timebills")
    if (result.status === 200) {
      bills.splice(0, bills.length, ...result.data)
    }
  });

  const total = computed(() => {
    return bills.map(b => b.billingRate * b.hours)
      .reduce((b, t) => t + b, 0);
  });

  function changeMe() {
    name.value = "+++";
    // alert(name);
  }

  function newItem() {
    bills.push({
      customerId: 1,
      employeeId: 1,
      hoursWorked: 5.0,
      rate: 114,
      work: "More work",
      billDate: "2023-05-08"
    });
    console.log(bills.length);
  }
</script>

<template>
  <header>
    <h3> Our App</h3>
  </header>

  <main>
    <h1>Hello From Vue</h1>
    <div>{{ name }}</div>
    <button class="btn" @click="changeMe">Change Me</button>
    <img src="/kodo.jpg" :alt="kodo" :title="kodo" />
    <button class="btn" @click="newItem">New Item</button>
    <table>
      <thead>
        <tr>
          <td>Hours </td>
          <td>Date </td>
          <td>Description </td>

        </tr>
      </thead>
      <tbody>
        <tr v-for="b in bills">
          <td>{{ b.hoursWorked }}</td>
          <td>{{ b.date }}</td>
          <td>{{ b.workPerformed }}</td>
        </tr>
      </tbody>

    </table>
    <div>Total: {{ formatMoney(total) }}</div>
  </main>
</template>

